using ChartExample.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace IEEE_EMB.Pages.Admin
{
    public class DashBoardModel : PageModel
    {
        public ChartJs BarChart { get; set; }
        public string ChartJson { get; set; }
        public ChartJs PieChart { get; set; }
        public string PieChartJson { get; set; }
        public DashBoardModel( )
        {
            BarChart = new ChartJs();
            PieChart = new ChartJs();
        }
        public void OnGet()
        {
            //Dictionary<string, int> favCodeEditors = new Dictionary<string, int>
            //{
            //    { "Visual Studio Code", 45 },
            //    { "IntelliJ IDEA", 25 },
            //    { "PyCharm", 15 },
            //    { "Eclipse", 10 },
            //    { "Atom", 5 }
            //};
            Dictionary<string, int> userRoles = new Dictionary<string, int>
            {
                { "Admin", 10 },
                { "Editor", 20 },
                { "Viewer", 70 }
            };



            //setUpBarChart(favCodeEditors);
            setUpDonutChart(userRoles);
        }
        private void setUpBarChart(Dictionary<string, int> dataToDisplay)
        {
            try
            {
                // 1. set up chart options
                BarChart.type = "bar";
                BarChart.options.responsive = true;

                // 2. separate the received Dictionary data into labels and data arrays
                var labelsArray = new List<string>();
                var dataArray = new List<double>();

                foreach (var data in dataToDisplay)
                {
                    labelsArray.Add(data.Key);
                    dataArray.Add(data.Value);
                }

                BarChart.data.labels = labelsArray;

                // 3. set up a dataset
                var firsDataset = new Dataset();
                firsDataset.label = "Number of votes";
                firsDataset.data = dataArray.ToArray();

                BarChart.data.datasets.Add(firsDataset);

                // 4. finally, convert the object to json to be able to inject in HTML code
                ChartJson = JsonConvert.SerializeObject(BarChart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error initialising the bar chart inside Index.cshtml.cs");
                throw e;
            }
        }
        private void setUpDonutChart(Dictionary<string, int> dataToDisplay)
        {
            try
            {
                // 1. set up chart options
                BarChart.type = "pie";
                BarChart.options.responsive = true;

                // 2. separate the received Dictionary data into labels and data arrays
                var labelsArray = new List<string>();
                var dataArray = new List<double>();

                foreach (var data in dataToDisplay)
                {
                    labelsArray.Add(data.Key);
                    dataArray.Add(data.Value);
                }

                BarChart.data.labels = labelsArray;

                // 3. set up a dataset
                var firsDataset = new Dataset();
                firsDataset.label = "Number of votes";
                firsDataset.data = dataArray.ToArray();

                BarChart.data.datasets.Add(firsDataset);

                // 4. finally, convert the object to json to be able to inject in HTML code
                ChartJson = JsonConvert.SerializeObject(BarChart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error initialising the bar chart inside Index.cshtml.cs");
                throw e;
            }
        }

    }
}
