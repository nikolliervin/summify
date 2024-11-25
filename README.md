# Summify API Setup Guide

## Prerequisites

Before running the API, you need to ensure the following:

1. **.NET SDK**: Ensure you have the .NET SDK installed. If not, download and install it from [here](https://dotnet.microsoft.com/download). For this project .NET 8.0 was used.
2. **Ollama Models**: You need to install Ollama models on your system to be able to use the api.

## 1. Clone the Repository

1. First, clone the repository containing the Summify API:
```bash
git clone https://github.com/yourusername/summify-api.git
cd summify-api/Summify
```

2. Install .NET Dependencies

   Ensure that all necessary .NET dependencies are installed by running the following command:
   ```bash
   dotnet restore

## 2. Install Ollama Models

To use Ollama models for summarization, follow these steps:

### 2.1 Install Ollama CLI

- **Windows**: Download the [Ollama CLI installer](https://ollama.com/) from here.
- **Mac**: Use Homebrew to install Ollama by running:

  ```bash
  brew install ollama

- **Linux**: Follow the installation instructions provided on [Ollama's GitHub](https://github.com/ollama/ollama).

### Suggested Models for Summarization

You can choose any model for summarization. I recommend the following models:

1. **tinydolphin**  
   A compact 1.1B model based on TinyLlama, trained on the Dolphin 2.8 dataset. Ideal for quick summarization.

2. **tinyllama**  
   A 1.1B Llama model trained on 3 trillion tokens. Good for quick summarization.

3. **llama 3**  
   A larger model for in-depth, detailed summaries with broader context understanding. It provides better accuracy but may be slower depending on your hardware.

## 3. Test Ollama

To test if Ollama is working properly, run the following command in your terminal:

```bash
ollama run tinydolphin
```
If everything is working, you should see this on your terminal:

```bash
>>> Send a message (/? for help)
```

## 4. Run the .NET Web api

To run the Summify .NET Web API, follow these steps:

Navigate to the `Summify` directory:


```bash
cd summify-api/Summify
```
And run the api using: 

```bash
dotnet run
```
