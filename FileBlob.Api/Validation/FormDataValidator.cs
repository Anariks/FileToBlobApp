using FluentValidation;
using Models;

namespace Api.Validation;

public class FormDataValidator : AbstractValidator<FormData>
{
    public FormDataValidator()
    {
        RuleFor(x => x.email).EmailAddress().WithMessage("Invalid Email");
        RuleFor(x => x.file)
            .Must(
                file =>
                    Path.GetExtension(file.FileName.ToLower()) == ".docx"
                    && file.ContentType
                        == "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            )
            .WithMessage("Not a .docx file");
    }
}
