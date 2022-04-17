namespace MySpace.Api.Application.Persistence;

public interface CounterRepository
{
    int GenerateArticleId();
    int GenerateCommentId();
    int GenerateProjectId();
    int GenerateJobId();
}