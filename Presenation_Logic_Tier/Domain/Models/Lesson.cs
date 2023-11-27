using System.Text.Json.Serialization;

namespace Domain.Models;

public class Lesson
{
    public long Date { get; set; }
    public string Description { get; set; }
    public Homework Homework { get; set; }
    public string Topic { get; set; }

    public Lesson(long date, string description, string topic, Homework homework)
    {
        Date = date;
        Description = description;
        Topic = topic;
        Homework = homework;
    }
    public Lesson(long date, string description, string topic)
    {
        Date = date;
        Description = description;
        Topic = topic;
    }
    [JsonConstructor]

    public Lesson(){}
}