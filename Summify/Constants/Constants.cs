public static class Constants{
    // public static string GetSummaryPrompt(int numberOfSentences, string text, string additionalPrompt = ""){   
    //     return $"You are a summarization model. Please summarize the following text in exactly {numberOfSentences} sentences. Only provide the summarized text, no additional statements or introductions. Do not say 'Here is the summarized text' or any similar phrase. Your response should be the summarized text only, with no explanations or additional context. {additionalPrompt} This is the text: {text}";
    // }

    public static string GetSummaryPrompt(int numberOfSentences, string text, string additionalPrompt = ""){
        Console.Write(numberOfSentences);
        return $@"
            You are a summarization model. Your task is to summarize the following text in exactly {numberOfSentences} sentences. 
            - Please do not exceed or go below the specified number of sentences.
            - Focus on capturing only the key points of the text.
            - Do not add any introductory or closing sentences such as 'Here is the summarized text'.
            - Only provide the summarized text in the exact number of sentences requested, no more, no less.

            {additionalPrompt}

            This is the text:  
            {text}";
}




    public static string GetArticleSummaryBonusPrompt(){
        return "This is gonna be a news article and its gonna contain a lot of html tags, ignore all the tags and give me the article.";
    }
    public static string MockPrompt(){
        return $"Hello!";
    }
}