using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fusion.WorldCheckOne.ApiClient;
using log4net;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.Name);

        public string WorldCheckProfileId { get; set; }

        private static WorldCheckOneApiClient mWorldCheckOneApiClient;

        private WcoProfile profile;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs ex)
        {
            var apiKey = "c408014e-5551-4aac-b522-1c2ade67badb";
            var apiSecret = "eb6o4rhK8CAjy4vAHPdkGT84snN/l90Dery0ZUW+xowjNtS4xuQw+zF92EkoUm74+RbhJH1WUQtbRrd85NZnqg==";
            //
            Log.Info("Creating client");
            try
            {
                var dir = SharedData.GetCacheDirectory();
                //
                mWorldCheckOneApiClient = new WorldCheckOneApiClient(apiKey, apiSecret, cacheDirectory: dir);
            }
            catch (Exception e)
            {
                Log.Error("Failed to initialised the APIClient", e);
                //
                return;
            }
            //
            profile = mWorldCheckOneApiClient.GetProfile(WorldCheckProfileId);
            //



            TreeNode tNode;
            Profile2Tree(profile, true);
            treeView1.ExpandAll();
        }

        private TreeNode Profile2Tree(WcoProfile profile, bool associateLoop)
        {
            //creates name as top node
            var nodeTop = treeView1.Nodes.Add(Guid.NewGuid().ToString(), profile.Name);

            // creates addresses
            var treeNodeAddresses = nodeTop.Nodes.Add("Addresses");
            foreach (var item in profile.Addresses)
            {
                var treeNodeAddress = treeNodeAddresses.Nodes.Add("Address");
                treeNodeAddress.Nodes.Add(item.City);
                treeNodeAddress.Nodes.Add(item.PostCode);
                treeNodeAddress.Nodes.Add(item.Region);
                treeNodeAddress.Nodes.Add(item.Street);
                treeNodeAddress.Nodes.Add(item.Country.Name);
            }

            // add's age
            nodeTop.Nodes.Add("Age:" + profile.age);

            // creates country links
            var treeNodeLink = nodeTop.Nodes.Add("CountryLinks");

            foreach (var x in profile.CountryLinks)
            {
                var type = treeNodeLink.Nodes.Add(x.CountryText);
                type.Nodes.Add(x.Type);
            }
            // creates reports
            var report = nodeTop.Nodes.Add("Report");
            report.Nodes.Add(profile.Reports);

            
            // creates sources
            var sources = nodeTop.Nodes.Add("Sources");
            foreach (var wcoProviderSource in profile.Sources)
            {
                var treeNode = sources.Nodes.Add(wcoProviderSource.Name + "-----Click for details");
                treeNode.Tag = wcoProviderSource.Type.Category.Description;
                treeNode.Nodes.Add("Time of Data Creation:" + wcoProviderSource.CreationDate.ToString());

            }
            //creates links
            var links = nodeTop.Nodes.Add("WebLinks");
            foreach (var link in profile.WebLinks)
            {
                var treeNodeUrl = links.Nodes.Add(link.Uri);
                treeNodeUrl.Tag = link.Uri;
            }
            //creates associates
            var associates = nodeTop.Nodes.Add("Associates");
            foreach (var associate in profile.Associates)
            {
                associate.GetExtra(mWorldCheckOneApiClient);
                var trNode = associates.Nodes.Add(associate.Profile?.Name ?? associate.TargetEntityId);
                trNode.Tag = associate.TargetEntityId;
                
            }
            
            return nodeTop;
        }

        
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var treeNode = treeView1.GetNodeAt(e.X, e.Y);
            if (treeNode == null)
            {
                return;
            } 
            //
            if (treeNode.Tag is string str)
            {
                if (str.StartsWith("http://"))
                {
                    var form2 = new Form2
                    {
                        uri = str
                    };
                    form2.Show();
                }
                else if (str.StartsWith("e_tr_wc"))
                {
                    var form1 = new Form1
                    {
                        WorldCheckProfileId = str
                    };
                    form1.Show();
                }
                else
                {
                    MessageBox.Show(str);
                }
            }
        }

        

       

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            var treeNode = treeView1.GetNodeAt(e.X, e.Y);
            if (treeNode == null)
            {
                return;
            }
            if (treeNode.Tag is string header)
            {
                textBox1.Text = header;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var source = @"C:\Code\Fusion.WorldCheckOne\cache\reference_profile_" + WorldCheckProfileId + ".json";
            //if (File.Exists(source))
            //{
            //    var destination = @"C:\Code\Fusion.WorldCheckOne\currantCache\reference_profile_" + WorldCheckProfileId + ".json";
            //    //
            //    if (!File.Exists(destination))
            //    {
            //        File.Copy(source, destination);

            //    }

            //}

            SaveProfile(profile.entityId);

            var sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Server=fs-swsql02;Database=WorldCheckApi;Trusted_Connection=True";
            //
            sqlConnection.Open();
            
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
UPDATE 
    Profiles
SET
    Archived = 1
WHERE 
    Id = @id;
";
            sqlCommand.Parameters.Add("@id", WorldCheckProfileId);

            sqlCommand.ExecuteNonQuery();



            sqlConnection.Close();
        }

        public void SaveProfile(string entityId)
        {
            var path = $@"C:\Code\Fusion.WorldCheckOne\cache\reference_profile_{entityId}.json";
            var json = File.ReadAllText(path);
            //
            var sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Server=fs-swsql02;Database=WorldCheckApi;Trusted_Connection=True";
            //
            sqlConnection.Open();
            //
            var sqlCommandInsertOrUpdate = new SqlCommand();
            sqlCommandInsertOrUpdate.Connection = sqlConnection;

            //
            var sqlCommandCheckForExistingProfile = new SqlCommand();
            sqlCommandCheckForExistingProfile.Connection = sqlConnection;
            sqlCommandCheckForExistingProfile.CommandText = @"
SELECT 
    Id
FROM
    Profiles 
WHERE 
    Id=@id
";
            sqlCommandCheckForExistingProfile.Parameters.Add("@id", entityId);
            //
            var existingResult = sqlCommandCheckForExistingProfile.ExecuteReader();
            if (existingResult.Read())
            {
                sqlCommandInsertOrUpdate.CommandText = @"
UPDATE 
    Profiles 
SET 
    Profile=@profile,
    Created=CURRENT_TIMESTAMP
WHERE
    Id=@id
";

            }
            else
            {
                sqlCommandInsertOrUpdate.CommandText = @"
INSERT INTO
    Profiles (Id, Profile)
VALUES
    (@id, @profile);
";
            }
            existingResult.Close();
            //
            sqlCommandInsertOrUpdate.Parameters.Add("@id", entityId);
            sqlCommandInsertOrUpdate.Parameters.Add("@profile", json);
            //
            var insertOrUpdateResult = sqlCommandInsertOrUpdate.ExecuteNonQuery();
            //
            sqlConnection.Close();
        }
    }
}
