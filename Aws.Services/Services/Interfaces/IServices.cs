namespace Aws.Services.Services;

public interface IServices<Result,Parameter>
{
    Task<Result> Execute(Parameter parameter, CancellationToken cancellationToken = default);
}

public interface IServices<Result>
{
    Task<Result> Execute(CancellationToken cancellationToken = default);
}
