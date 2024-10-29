using AiComp.Domain.Entities;
using GroqSharp.Models;
using Microsoft.ML;

namespace AiComp.Application.Interfaces.Service;
public interface IAiServices
{
    Task<string> GetNextMoodQuestions(string userPrompt);
    Task<string> GetResponseFromAiOnDailyMood();
    Task<string> ChatCompletionAsync(List<MoodMessage> messages);
    Task<string[]> GetAllQuestions();
}
