public static class Constants{
    public static string GetSummaryPrompt(int numberOfSentences, string text, string additionalPrompt = ""){
        return $"You are a summarization model. Please summarize the following text based only on the sentences provided. Focus on capturing the key points from those sentences. You should summarize the text in exactly {numberOfSentences} sentences! Just give me the summarized text and nothing else. Do not say: Here is the summarized text:. Your response should only contain the summarized text! {additionalPrompt}.  This is the text:   {text}";
    }
    public static string GetArticleSummaryBonusPrompt(){
        return "This is gonna be a news article and its gonna contain a lot of html tags, ignore all the tags and give me the article.";
    }
    public static string MockPrompt(){
        return $"Hello!";
    }
}