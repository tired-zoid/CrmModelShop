using CrmBI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUI
{
    public partial class Main : Form
    {

        CrmContext db;

        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
        }

        private void productToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var CatalogProduct = new Catalog<Product>(db.Products, db);
            CatalogProduct.Show();
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var CatalogSeller = new Catalog<Seller>(db.Sellers, db);
            CatalogSeller.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var CatalogCustomer = new Catalog<Customer>(db.Customers, db);
            CatalogCustomer.Show();
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var CatalogCheck = new Catalog<Check>(db.Checks, db);
            CatalogCheck.Show();
        }

        private void customerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }

        private void sellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }
        }

        private void productAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }
    }
}
