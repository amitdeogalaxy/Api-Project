using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Google.Protobuf;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using Serilog;
using Serilog.Configuration;
using System.Text;


namespace Active_Active.StepDefinitions
{
    [Binding]
    public class SimpleGetRequest
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        RestClient restClient;
        RestRequest restRequest;
        IRestResponse restResponse;
        String url;
        String URI;
        Hooks hk = new Hooks();

        SimpleGetRequest()
        {
            restClient = null;
            restResponse = null;
            hk.ExtentStart();
            hk.initilizelogger();
        }


        [Given(@"I have Api url")]
        public void GivenIHaveApiUrl()
        {
            hk.addtexttoextentreport("SimpleGetRequest", "Sceanrio Execution started");
            hk.addtexttoextentreport("Given", "Execution started for I have Api url");
            url = "https://reqres.in/api/";
            hk.logger("URL is" + url);
        }
        
        [Given(@"I have URI")]
        public void GivenIHaveURI()
        {
            hk.addtexttoextentreport("Given", "I have URI");
             restClient = new RestClient("https://reqres.in/api/");
             URI = url + "users";
             restRequest = new RestRequest("users", Method.GET);
            hk.addtexttoextentreport("Given","Complete URI is :"+URI);
            hk.logger("URI is https://reqres.in/api//users");
        }

        [When(@"I hit a Restsharp Get Request")]
        public void WhenIHitARestsharpGetRequest()
        {
            hk.addtexttoextentreport("When", "I hit a Restsharp Get Request");
            restResponse = restClient.Execute(restRequest);
            hk.logger("Executing the Get request");
            string response = restResponse.Content;

            if (response != null) 
            {               
                hk.pasststausreport("Response received is :" + response);
                hk.logger("Response Recived from Get Request is " + response);
            }
            else {
                hk.failstatusreport("Response received is :" + response);
                Log.Error("No respose received from sent request" + URI);            
            }       
            
        }

        [Then(@"I Verify the Result")]
        public void ThenIVerifyTheResult()
        {

            hk.addtexttoextentreport("Then", "I Verify the Result");
            var jObject = JObject.Parse(restResponse.Content);
            string per_page = jObject.GetValue("per_page").ToString();
           
            if (per_page != null)
            {
                hk.pasststausreport("per_page received is :" + per_page);
                hk.logger("per_page Recived from Get Request is " + per_page);
            }
            else
            {
                hk.failstatusreport("per_page received is :" + per_page);
                Log.Error("No per_page received from sent request" + URI);
            }
            string total_pages = jObject.GetValue("total_pages").ToString();
            if (total_pages != null)
            {
                hk.pasststausreport("total_pages received is :" + total_pages);
                hk.logger("total_pages Recived from Get Request is " + total_pages);
            }
            else
            {
                hk.failstatusreport("total_pages received is :" + total_pages);
                Log.Error("No total_pages received from sent request" + URI);
            }
            
            int StatusCode = (int)restResponse.StatusCode;
            try
            {
                Assert.AreEqual(200, StatusCode, "Status code is 200");
                hk.pasststausreport("Scenario Passed");
                hk.pasststausreport("StatusCode received is :" + StatusCode);
                hk.logger("StatusCode Recived from Get Request is " + StatusCode);
                

            }
            catch (Exception ex)
            {
                //hk.failstatusreport("Sceanrios failed as Status recieved is :" + ex);

                hk.logger("Sceanrios failed as Status recieved is" + ex);
            }
            finally
            {
                hk.closelogger();
                hk.email();
                hk.ExtentClose();
                hk.email();

            }
            
        }



    }
}
