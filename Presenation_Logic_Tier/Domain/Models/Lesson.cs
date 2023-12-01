using System.Text.Json.Serialization;

namespace Domain.Models;

public class Lesson
{
    public string Id { get; set; }
    public long Date { get; set; }
    public string Description { get; set; }
    public Homework Homework { get; set; }
    public string Topic { get; set; }

    public Lesson(string id, long date, string description, string topic,Homework homework )
    {
        Id = id;
        Date = date;
        Description = description;
        Topic = topic;
        Homework = homework;
    }

    public Lesson(string id, long date, string description, string topic)
    {
        Id = id;
        Date = date;
        Description = description;
        Topic = topic;
    }
    [JsonConstructor]

    public Lesson(){}
}