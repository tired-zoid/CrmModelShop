﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBI.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random rnd = new Random();
        bool isWorking = false;
        List<Task> tasks = new List<Task>();

        CancellationTokenSource cancelTokenSource;
        CancellationToken token;
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        public int ClientSpeed { get; set; } = 100;
        public int CashDeskSpeed { get; set; } = 100;
        public ShopComputerModel()
        {
           var sellers =  Generator.GetNewSellers(20);
            Generator.GetNewProducts(1000);
            Generator.GetNewCustomers(100);
            
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
           
            for(int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue(), null));
            }

        }
        public void Start()
        {
            isWorking = true;

            tasks.Add(new Task(()=> CreateCarts(10, token)));
            
            tasks.AddRange(CashDesks.Select(c => new Task(() => CashDeskWork(c, token))));
            foreach(var task in tasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            isWorking = false;
            cancelTokenSource.Cancel();
        }


        private void CashDeskWork(CashDesk cashDesk, CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(CashDeskSpeed);
                }
            }

        }

        private void CreateCarts(int customerCount, CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                var customers = Generator.GetNewCustomers(customerCount);


                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);

                    foreach(var prod in Generator.GetRandomProducts(10,30))
                    {
                        cart.Add(prod);
                    }
                    var cash = CashDesks[rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(ClientSpeed);
            }
          
        }
    }
}
