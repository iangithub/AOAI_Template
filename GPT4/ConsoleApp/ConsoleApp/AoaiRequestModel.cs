﻿using Newtonsoft.Json;

public class AoaiRequestModel
{
    [JsonProperty(PropertyName = "messages")]
    public List<Message> Messages { get; private set; }

    /// <summary>
    /// between 0 and 2，default是1，數值愈高隨機性較強，設為0表示每次回應都相同
    /// </summary>
    [JsonProperty(PropertyName = "temperature")]
    public float Temperature { get; set; }

    /// <summary>
    /// between 0 and 1，default是1，數值愈高，輸出的回答愈多樣化
    /// 用於控制語言模型在嘗試預測下一個單詞時考慮多少個不同的單詞(token)。
    /// 例如:top_p值設置為 0.5，則語言模型將僅考慮接下來可能出現的 50% 最有可能出現的單詞(token)。
    /// 如果將top_p值設置為 0.9，則語言模型將考慮 90% 最有可能的單詞(token)。
    /// </summary>
    [JsonProperty(PropertyName = "top_p")]
    public float Top_p { get; set; }

    /// <summary>
    /// between -2.0 and 2.0，數值愈高，回答的用字會較冷僻
    /// </summary>
    [JsonProperty(PropertyName = "frequency_penalty")]
    public int Frequency_Penalty { get; set; }

    /// <summary>
    /// between -2.0 and 2.0，數值愈高，會避免重覆使用過的字，例如輸入時用過的字或是回答已用過的字
    /// </summary>
    [JsonProperty(PropertyName = "presence_penalty")]
    public int Presence_Penalty { get; set; }
    [JsonProperty(PropertyName = "max_tokens")]
    public int Max_Tokens { get; set; }

    public AoaiRequestModel(string sysContent)
    {
        /*
         * sysContent Sample : 
         * "你是一位客服人員，我會給你準備要回答客戶的答案，請你進行內容文字的調整並以客服語氣產生500個字以內的回答"
         * You are a customer service representative, and I will provide you with the answer that I have prepared for the customer. 
         * Please adjust the content wording and generate a response in a customer service tone within 500 words.
         */

        Messages = new List<Message>
            {
                new Message() { Role = "system", Content = sysContent }
            };
        Temperature = 0.8f;
        Top_p = 0.95f;
        Frequency_Penalty = 0;
        Presence_Penalty = 0;
        Max_Tokens = 1000;
    }

    public void AddUserMessages(string message)
    {
        this.Messages.Add(new Message() { Role = "user", Content = message });
    }
    public void AddGptMessages(string message)
    {
        this.Messages.Add(new Message() { Role = "assistant", Content = message });
    }
}

public class Message
{
    /// <summary>
    /// system/assistant/user
    /// </summary>
    [JsonProperty(PropertyName = "role")]
    public string Role { get; set; }
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; }
}

