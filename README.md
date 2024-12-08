# PromptClient

A .Net Core library for communicating with AI API services such as the OpenAI API.  

**Note from author**: Currently the released version, 0.1.x, has an API interface that likely will change. I have noticed that a few people downloaded it immediately.  If you are reading this and it is v0.1.x version still, then expect the interface to change.  I want to make a more generic interface so I can support more than one service and a simpler interface than the response and request formats currently. Before 1.0, I expect to make drastic changes to the API surface to make it a better experience. If for someone reason you immediately downloaded and started using this API, for one...you are crazy...it had 0 downloads....that's not safe...but if you do happen to be human, I oppologize in advance for immediately changing the interface of the API after release.

## Usage

```c#
    var apiKey = "YOUR_API_KEY_HERE";
    var creds = new AICredentials(apiKey);
    using var client = new OpenAIClient(httpClient, creds);
    var response = await client.SendAsync(
        new List<AIMessage> {
            new AIMessage {
                Content = "My LLM Prompt"
            }
        }
    );
    if (response.IsError) 
    {
        var error = response.UnwrapError();
        //Do something with error        
        return;
    }
    var apiResponse = response.Unwrap();
```