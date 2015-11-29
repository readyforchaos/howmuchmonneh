using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using HowMuchMonneh.Models;
using Newtonsoft.Json;
using static System.Int32;

namespace HowMuchMonneh.WebService
{
    public class IncomeWebService
    {

        public async Task<T> InvokeRequestResponseService<T>(string gender, string age) where T : class
        {
            using (var client = new HttpClient())
            {
                var personValues = new[]
                    {
                        new Person
                        {
                            Gender = gender,
                            Age = Parse(age),
                            Income = 0
                        }
                };
                var propertyCount = typeof(Person).GetProperties().Count();
                var convertValues = new string[personValues.Count(), propertyCount];
                for (var i = 0; i < convertValues.GetLength(0); i++)
                {
                    convertValues[i, 0] = personValues[i].Gender;
                    convertValues[i, 1] = personValues[i].Age.ToString();
                    convertValues[i, 2] = personValues[i].Income.ToString();

                }

                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, PersonTable>() {
                        {
                            "input1",
                            new PersonTable()
                            {
                                ColumnNames = new [] {"gender", "age", "income"},
                                Values = convertValues
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "JMX7o3Tgs3D/FiAdb+NqaXBlXZDd7D7Gwe+aOszbXNNrvaMd5V3sBjCHhOVIdNKpjDtYXY7bzTBCE/qF5digvg=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/033d6f8d8b064d6ea124312bd0177ae2/services/5730cfea87244590bb746f0bfbf3b3f1/execute?api-version=2.0&details=true");


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var resultOutcome = JsonConvert.DeserializeObject<T>(result);
                    Debug.WriteLine("Result: {0}", result);
                    return resultOutcome;
                }
                else
                {
                    Debug.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Debug.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine(responseContent);
                    return null;
                }
            }
        }
    }
}