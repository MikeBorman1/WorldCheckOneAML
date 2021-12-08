using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fusion.WorldCheckOne.ApiClient;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public bool beenClicked = false;
        public WorldCheckOneApiClient Client { get; set; }
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
            for (int i = 1; i < 32; i ++)
            {
                comboBox1.Items.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i);
            }
            for (int i = 2017; i > 1867; i--)
            {
                comboBox3.Items.Add(i);
            }
            List<string> countries = (new List<string>
            {
                "Åland Islands",
                "Albania",
                "Algeria",
                
                "American Samoa",
                "Andorra",
                "Angola",
                "Anguilla",
                "Antarctica",
                "Antigua and Barbuda",
                "Argentina",
                "Armenia",
                "Aruba",
                "Australia",
                "Austria",
                "Azerbaijan",
                "Bahamas",
                "Bahrain",
               "Bangladesh",
                "Barbados",
                "Belarus",
                "Belgium",
                "Belize",
                "Benin",
                "Bermuda",
                "Bhutan",
                "Bolivia", 
                "Bonaire", 
                "Bosnia and Herzegovina",
                "Botswana",
                "Bouvet Island",
                "Brazil",
                "British Indian Ocean Territory",
                "Brunei Darussalam",
                "Bulgaria",
                "Burkina Faso",
                "Burundi",
                "Cambodia",
                "Cameroon",
                "Canada",
                "Cape Verde",
                "Cayman Islands",
                "Central African Republic",
                "Chad",
                "Chile",
                "China",
                "Christmas Island",
                "Cocos Islands",
                "Colombia",
                "Comoros",
                "Congo",
                "Congo", 
                "Cook Islands",
                
                "Tuvalu",
                "Uganda",
                "Ukraine",
                "United Arab Emirates",
                "United Kingdom",
                "United States",
               "United States Minor Outlying Islands",
                "Unspecified Nationality",
                "Uruguay",
                "Uzbekistan",
                "Vanuatu",
                "Venezuela", 
                "Viet Nam",
                "Virgin Islands", 
                "Virgin Islands", 
                "Wallis and Futuna",
                "Western Sahara",
                "Yemen",
                "Zambia",
                "Zimbabwe"
            });
            foreach (string name in countries)
            {
                comboBox5.Items.Add(name);
                comboBox6.Items.Add(name);
                comboBox4.Items.Add(name);
            }

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string gender;
            if (radioButton1.Checked)
            {
                gender = "MALE";}
            else if (radioButton2.Checked)
            {
                gender = "FEMALE";
            }
            else
            {
                gender = "UNSPECIFIED";
            }

            var dateOfBirth = comboBox3.Text + "-" + comboBox2.Text + "-" + comboBox1.Text;
            var countryLoc = comboBox4.Text.Trim();
            var name = textBox1.Text.Trim();
            var groupId = "0a3687d0-5d37-1694-9754-215600000395";
            var doScreening = Client.DoScreening(groupId, "INDIVIDUAL", name, gender , dateOfBirth, countryLoc);
        }

       

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (!beenClicked)
            {
                textBox1.Text = String.Empty;
                beenClicked = true;
            }
        }
    }
}
