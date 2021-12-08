using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fusion.WorldCheckOne.ApiClient;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        
        public string EntityId;
        private static WorldCheckOneApiClient mWorldCheckOneApiClient;

        public FormMain()
        {
            InitializeComponent();

        }

        private void buttonGetProfile_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            var f = new Form1() {WorldCheckProfileId = id};
            f.Show();
        }

        private void FormMain_Load(object sender, EventArgs ex)
        {
            
            var apiKey = "c408014e-5551-4aac-b522-1c2ade67badb";
            var apiSecret = "eb6o4rhK8CAjy4vAHPdkGT84snN/l90Dery0ZUW+xowjNtS4xuQw+zF92EkoUm74+RbhJH1WUQtbRrd85NZnqg==";
            //
            
            try
            {
                var dir = SharedData.GetCacheDirectory();
                //
                mWorldCheckOneApiClient = new WorldCheckOneApiClient(apiKey, apiSecret, cacheDirectory: dir);
            }
            catch (Exception)
            {
                
                //
                return;
            }
            
        }
        // cut of the name
        private string TrimString(string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength) + "...";
            }
            //
            return str;
        }
        public string CalcCountryInfluence()
        {
            var stringBuilder = new StringBuilder();
            var profile = mWorldCheckOneApiClient.GetProfileFromCache(EntityId);
            List<string> countries = profile.CountryInfluence;
            countries.RemoveAll(c => c == "UNKNOWN");
            foreach (var associate in profile.Associates)
            {
                var ass = mWorldCheckOneApiClient.GetProfileFromCache(associate.TargetEntityId);
                if (ass == null)
                {
                    continue;
                }
                foreach (string country in ass.CountryInfluence)
                {
                    countries.Add(country);
                }
            }
            foreach (var item in countries.GroupBy(c => c))
            {
                float count = item.Count();
                stringBuilder.AppendLine(item.First() + "\t" + Math.Round(((count / countries.Count) * 100),2));

            }
            return stringBuilder.ToString();
            

        }
        //calculates the risk based on associate risk
        private int GetBackFlowRisk(List<WcoAssociates> associateList, WorldCheckOneApiClient client, float risk, float assNum)
        {
            float riskAcc = 0;
            foreach (var associate in associateList)
            {

                var profile = client.GetProfileFromCache(associate.TargetEntityId);
                if (profile != null)
                {
                    riskAcc += profile.Risk;
                    var isPoliticallyEsposedPerson = profile.Sources.Any(s => s.Type.Category.Name == "PEP");
                    var isSanctioned = profile.Sources.Any(s => s.Type.Category.Name == "Sanctioned");
                    if (isSanctioned)
                    {
                        riskAcc += 50;
                    }
                }
                else
                {
                    riskAcc += 0;
                    assNum -= 1;
                    if (assNum == 0 || assNum == -1)
                    {
                        assNum = 1;
                    }
                }

                
            }
            riskAcc = (riskAcc / assNum);
            riskAcc = riskAcc - risk;
            risk += riskAcc;
            return (int)Math.Round(risk, 2);
        }
        //*******************Opens form one on double click and creates popout if clicked on risk to show %composition
         private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HandleListView1_MouseDoubleClick(sender, e);
            
        }

        private void HandleListView1_MouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var listViewItem = listView1.GetItemAt(mouseEventArgs.X, mouseEventArgs.Y);
            var listViewSubItem = listView1.GetItemAt(mouseEventArgs.X, mouseEventArgs.Y).GetSubItemAt(mouseEventArgs.X, mouseEventArgs.Y);
            if (listViewSubItem.Tag is string t)
            {
                MessageBox.Show(t);
                return;
            }
            //
            var id = listViewItem.Tag as string;
            //
            var f = new Form1() { WorldCheckProfileId = id };
            f.Show();
        }
        //***************************

        // creates node for visualisation
        public class NodeCreate
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("group")]
            public int Group { get; set; }

            public NodeCreate(string id, int group)
            {
                Id = id;
                Group = group;
            }
        }
        //
        public class LinkCreate
        {
            [JsonProperty("source")]
            public string Source { get; set; }

            [JsonProperty("target")]
            public string Target { get; set; }

            [JsonProperty("value")]
            public int Value { get; set; }

            public LinkCreate(string source, string target, int value)
            {
                Source = source;
                Target = target;
                Value = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool nodeCreated;
            bool linkcreated;
            var nodesList = new List<NodeCreate>();
            var linksList = new List<LinkCreate>();
            //
            var profile = mWorldCheckOneApiClient.GetProfile(EntityId);
            //add first node
            nodesList.Add(new NodeCreate(profile.Name, 2));
            foreach (var associates in profile.Associates.Take(100000))
            {
                var associate = mWorldCheckOneApiClient.GetProfile(associates.TargetEntityId);

                // create associate node
                AddNode(new NodeCreate(associate.Name, GetColour(associate.EntityType)));
                
                // create link back to associate node
                AddLink(new LinkCreate(profile.Name, associate.Name, 2));
                //
                Itterate(associate, true);
            }
            // creates associates of associates nodes
            void AddNode(NodeCreate node)
            {
                if (!nodesList.Any(n => n.Id == node.Id))
                {
                    nodesList.Add(node);
                }
            }

            void AddLink(LinkCreate link)
            {
                if (!linksList.Any(l => (l.Source == link.Source && l.Target == link.Target) || (l.Source == link.Target && l.Target == link.Source)))
                {
                    linksList.Add(link);
                }
            }

            void Itterate(WcoProfile profiles, bool recurse = false)
            {
                nodeCreated = false;
                foreach (var associates in profiles.Associates)
                {
                    var associate = mWorldCheckOneApiClient.GetProfileFromCache(associates.TargetEntityId);
                    if (associate == null)
                    {
                        continue;
                    }
                    // create associate node
                    AddNode(new NodeCreate(associate.Name, GetColour(associate.EntityType)));
                    
                    //nodesList.Add(new NodeCreate(associate.Name, 1));
                    AddLink(new LinkCreate(profiles.Name, associate.Name, 1));
                    //
                    if (recurse)
                    {
                        Itterate(associate);
                    }
                }
                
            }
            
            //
            var jsonData = new
            {
                nodes = nodesList,
                links = linksList
            };
            var json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
//            
           File.WriteAllText(@"C:\Code\Fusion.WorldCheckOne\graphtest\data.json", json);
            
            Process.Start(@"C:\Code\Fusion.WorldCheckOne\graphtest\graph.html");


        }

        private int GetColour(string associateEntityType)
        {
            switch (associateEntityType)
            {
                case "INDIVIDUAL":
                    return 1;
                case "ORGANISATION":
                    return 3;
                default:

                    return 8;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            var listViewItem = listView1.GetItemAt(e.X, e.Y);
            EntityId = listViewItem.Tag.ToString();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadProfiles();
        }

        public void LoadProfiles()
        {
            listView1.Items.Clear();
            
            //
            //var files = Directory.GetFiles(@"C:\Code\Fusion.WorldCheckOne\currantCache");
            // get all files in directory
            
            var wcoProfiles = new List<WcoProfile>();
            //foreach (var file in files)
            //{
            //    var json = File.ReadAllText(file);
            //    //
            //    var profile = JsonConvert.DeserializeObject<WcoProfile>(json);
            //    //
            //    profile.isSanctioned = profile.Sources.Any(s => s.Type.Category.Name.Contains("Sanctions"));
            //    profile.isIndividual = profile.EntityType.Contains("INDIVIDUAL");
            //    profile.isOrganisation = profile.EntityType.Contains("ORGANISATION");
                

            //    //
            //    wcoProfiles.Add(profile);
            //}
            wcoProfiles = GetFromDatabase();
            //Filtering
            var wcoProfilesFiltered = wcoProfiles;

            if (radioButtonSanctioned.Checked)
            {
                wcoProfilesFiltered = wcoProfiles.Where(p => p.isSanctioned).ToList();
            }
            if (Individual.Checked)
            {
                wcoProfilesFiltered = wcoProfiles.Where(p => p.isIndividual).ToList();
            }
            if (radioButton1.Checked)
            {
                wcoProfilesFiltered = wcoProfiles.Where(p => p.isOrganisation).ToList();
            }
            wcoProfilesFiltered = wcoProfilesFiltered.Where(p => p.Name.ToLowerInvariant().Contains(textBox2.Text.ToLowerInvariant())).ToList();
            textBox2.Text = null;
            foreach (var profile in wcoProfilesFiltered.OrderBy(p => p.Name))
            {
                
                string age;
                var Risk = profile.Risk;
                float backflowRisk = GetBackFlowRisk(profile.Associates, mWorldCheckOneApiClient, Risk, profile.AsNum);
                if (profile.age == 0)
                {
                    age = "n/a";
                }
                else
                {
                    age = profile.age.ToString();
                }
                var listViewItem = listView1.Items.Add(TrimString(profile.Name, 25));
                listViewItem.Tag = profile.entityId;
                listViewItem.SubItems.Add(profile.entityId);
                listViewItem.SubItems.Add(profile.EntityType);
                listViewItem.SubItems.Add(profile.Gender);
                var x = listViewItem.SubItems.Add(Risk.ToString());
                listViewItem.SubItems.Add(backflowRisk.ToString());
                listViewItem.SubItems.Add(age);
                x.Tag = profile.riskComp;

                listViewItem.SubItems.Add(profile.isSanctioned.ToString());

                listViewItem.UseItemStyleForSubItems = false;
                if (Risk <= 10) { listViewItem.SubItems[4].BackColor = Color.DarkGreen; }
                if (Risk <= 30 && Risk > 10) { listViewItem.SubItems[4].BackColor = Color.ForestGreen; }
                if (Risk <= 50 && Risk > 30) { listViewItem.SubItems[4].BackColor = Color.Yellow; }
                if (Risk <= 70 && Risk > 50) { listViewItem.SubItems[4].BackColor = Color.DarkOrange; }
                if (Risk <= 90 && Risk > 70) { listViewItem.SubItems[4].BackColor = Color.Red; }
                if (Risk > 90) { listViewItem.SubItems[4].BackColor = Color.Crimson; }

                if (backflowRisk <= 10) { listViewItem.SubItems[5].BackColor = Color.DarkGreen; }
                if (backflowRisk <= 30 && backflowRisk > 10) { listViewItem.SubItems[5].BackColor = Color.ForestGreen; }
                if (backflowRisk <= 50 && backflowRisk > 30) { listViewItem.SubItems[5].BackColor = Color.Yellow; }
                if (backflowRisk <= 70 && backflowRisk > 50) { listViewItem.SubItems[5].BackColor = Color.DarkOrange; }
                if (backflowRisk <= 90 && backflowRisk > 70) { listViewItem.SubItems[5].BackColor = Color.Red; }
                if (backflowRisk > 90) { listViewItem.SubItems[5].BackColor = Color.Crimson; }

                listViewItem.SubItems[7].BackColor = profile.isSanctioned ? Color.Aqua : Color.Chartreuse;




            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            // Finds all source catagories
            var allSources = wcoProfiles.SelectMany(p => p.Sources).ToList();
            var allSourceTypes = allSources.Select(s => s.Type).ToList();
            var distintSourceTypes = allSourceTypes.GroupBy(s => s.Name).Select(s => s.First()).ToList();
        }

        private void caseCreate_Click(object sender, EventArgs e)
        {
            var f = new Form3(){Client = mWorldCheckOneApiClient};
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var dir = @"C:\Code\Fusion.WorldCheckOne\currantCache\reference_profile_" + EntityId + ".json";
            //if (File.Exists(dir))
            //{
            //    File.Delete(dir);
            //}
            //LoadProfiles();

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
    Archived = 0
WHERE 
    Id = @id;
";
            sqlCommand.Parameters.Add("@id", EntityId);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            LoadProfiles();
        }

        private List<WcoProfile> GetFromDatabase()
        {
            int cache = 1;
            if (getCached.Checked)
            {
                cache = 0;
            }
            var x = getCached.Checked ? button2.Enabled = false : button2.Enabled = true;
            var y = getCached.Checked ? button3.Enabled = true : button3.Enabled = false;
            var sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Server=fs-swsql02;Database=WorldCheckApi;Trusted_Connection=True";
            //
            sqlConnection.Open();
            //
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = @"
SELECT 
    Id,
    Profile,
    Created,
    Archived
FROM
    Profiles
WHERE
    Archived = @cache
ORDER BY
    Id ASC

";
            sqlCommand.Parameters.Add("@cache", cache);
            var wcoProfiles = new List<WcoProfile>();
            var sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                
                
               var json = sqlDataReader["Profile"].ToString();
               var profile = JsonConvert.DeserializeObject<WcoProfile>(json);
               profile.isSanctioned = profile.Sources.Any(s => s.Type.Category.Name.Contains("Sanctions"));
               profile.isIndividual = profile.EntityType.Contains("INDIVIDUAL");
               profile.isOrganisation = profile.EntityType.Contains("ORGANISATION");
               wcoProfiles.Add(profile);
            }
            sqlDataReader.Close();
            //
            sqlConnection.Close();
            return wcoProfiles;
        }

        private void import_Click(object sender, EventArgs e)
        {
            var sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Server=fs-swsql02;Database=WorldCheckApi;Trusted_Connection=True";
            //
            sqlConnection.Open();

            var files = Directory.GetFiles(@"C:\Code\Fusion.WorldCheckOne\cache");
            // get all files in directory
            
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                //
                var profile = JsonConvert.DeserializeObject<WcoProfile>(json);
                //
                
                //
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"
INSERT INTO
    Profiles (Id, Profile)
VALUES
    (@id, @profile)
";
                sqlCommand.Parameters.Add("@id", profile.entityId);
                sqlCommand.Parameters.Add("@profile", json);
                //
                var result = sqlCommand.ExecuteNonQuery();
               
                //
                
            }

            sqlConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
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
            sqlCommand.Parameters.Add("@id", EntityId);

            sqlCommand.ExecuteNonQuery();



            sqlConnection.Close();
        }

        private void getCached_Click(object sender, EventArgs e)
        {
            if (getCached.Checked)
            {
                getCached.Checked = false;
            }
            else
            {
                getCached.Checked = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CalcCountryInfluence());
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) != Keys.Enter)
            {
                return;
            }
            //
            LoadProfiles();
        }
    }
}
