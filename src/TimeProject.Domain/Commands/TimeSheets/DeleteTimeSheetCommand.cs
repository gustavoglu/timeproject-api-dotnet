using TimeProject.Domain.Validations.Entities;

namespace TimeProject.Domain.Commands.TimeSheets
{
    public class DeleteTimeSheetCommand : TimeSheetCommand
    {
        public override bool IsValid()
        {
            return base.IsValid(new EntityIdValidation<DeleteTimeSheetCommand>().Validate(this));
        }
    }
}
