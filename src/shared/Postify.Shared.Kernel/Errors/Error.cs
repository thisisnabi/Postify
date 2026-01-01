namespace Postify.Shared.Kernel.Errors;

public readonly record struct Error
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Code { get; init; } = null!;
    public ErrorType Type { get; init; }
    public IDictionary<string, string>? Metadata { get; init; }

    public Error(string title, string description, string code ,ErrorType type, IDictionary<string, string>? metadata)
    {
        Title = title;
        Description = description;
        Code = code;
        Type = type;
        Metadata = metadata;
    }

    public static Error Failure(
        string title = "خطا در انجام عملیات",
        string description = "خطایی رخ داده است.",
        string code = "general.failure",
        ErrorType type = ErrorType.Failure,
        IDictionary<string, string>? metadata = null)
        => new Error(title, description, code, type, metadata);

    public static Error Validation(
        string title = "اعتبارسنجی",
        string description = "خطایی در اعتبار سنجی رخ داده است.",
        string code = "general.validation",
        ErrorType type = ErrorType.Validation,
        IDictionary<string, string>? metadata = null)
        => new Error(title, description, code, type, metadata);

    public static Error Unexpected(
        string title = "خطای غیرمنتظره",
        string description = "یک خطای غیرمنتظره رخ داده است.",
        string code = "general.unexpected",
        ErrorType type = ErrorType.Unexpected,
        IDictionary<string, string>? metadata = null)
        => new Error(title, description, code, type, metadata);

    public static Error NotFound(
        string title = "یافت نشد",
        string description = "منبع مورد نظر یافت نشد.",
        string code = "general.not_found",
        ErrorType type = ErrorType.NotFound,
        IDictionary<string, string>? metadata = null)
        => new Error(title, description, code, type, metadata);

    public override string ToString() => Description;
}
