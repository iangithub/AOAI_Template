// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text;

string az_OpenAi_Endpoint = "https://xxxx.openai.azure.com/openai/deployments";
string az_OpenAi_Key = "xxxxxx";
string az_OpenAi_Api_Version = "2023-03-15-preview";
string az_OpenAi_DeploymentName = "xxxxx";

Console.WriteLine("Hello, GPT4!");

string azureOpenApiEndpoint = $"{az_OpenAi_Endpoint}/{az_OpenAi_DeploymentName}/chat/completions?api-version={az_OpenAi_Api_Version}";

using (HttpClient client = new HttpClient())
{
    client.DefaultRequestHeaders.Add("api-key", az_OpenAi_Key);
    var requestModel = new AoaiRequestModel("你是一位客服人員，我會給你準備要回答客戶的答案，請你進行內容文字的調整並以客服語氣產生200個字以內的回答");
    requestModel.AddUserMessages("在6樓及B2有客服中心可以辦理外籍人士退稅服務");

    var json = JsonConvert.SerializeObject(requestModel);
    var data = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await client.PostAsync(azureOpenApiEndpoint, data);
    var responseContent = await response.Content.ReadAsStringAsync();

    var completion = JsonConvert.DeserializeObject<Completion>(responseContent);

    Console.WriteLine(completion.Choices[0].Message.Content);
}


