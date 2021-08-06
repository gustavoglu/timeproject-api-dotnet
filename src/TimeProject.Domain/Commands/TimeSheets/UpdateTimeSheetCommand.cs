using TimeProject.Domain.Validations.TimeSheets;

namespace TimeProject.Domain.Commands.TimeSheets
{
    public class UpdateTimeSheetCommand : TimeSheetCommand
    {
        public override bool IsValid()
        {
            return base.IsValid(new UpdateTimeSheetValidation().Validate(this));
        }
    }
}
