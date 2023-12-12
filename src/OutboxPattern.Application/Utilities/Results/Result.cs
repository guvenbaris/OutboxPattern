namespace OutboxPattern.Application.Utilities.Results;
public class Result<TData> : Result
{
    public TData Data { get; set; }
    public Result(TData data, string errorMessage, bool isSuccess) : base(errorMessage, isSuccess)
    {
        Data = data;
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; private set; }

    protected Result(string errorMessage, bool isSuccess)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static async Task<Result> Success()
    {
        return await Task.FromResult(new Result(string.Empty, true));
    }

    public static async Task<Result<TData>> Success<TData>(TData data)
    {
        return await Task.FromResult(new Result<TData>(data, string.Empty, true));
    }

    public static async Task<Result<TData>> Problem<TData>(string errorMessage)
    {
        return await Task.FromResult(new Result<TData>(default, errorMessage, false));
    }
    public static async Task<Result<TData>> Problem<TData>()
    {
        return await Task.FromResult(new Result<TData>(default, string.Empty, false));
    }

    public static async Task<Result> Problem(string errorMessage)
    {
        return await Task.FromResult(new Result(errorMessage, false));
    }

    public static async Task<Result> Problem()
    {
        return await Task.FromResult(new Result(string.Empty, false));
    }
}
