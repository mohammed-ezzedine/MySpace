namespace MySpace.Api.Application.Services;

public class Page<T>
{
    public List<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int TotalNumberOfPages { get; set; }
}