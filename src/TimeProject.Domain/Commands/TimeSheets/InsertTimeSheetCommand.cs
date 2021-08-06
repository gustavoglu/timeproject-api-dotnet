using TimeProject.Domain.Validations.TimeSheets;

namespace TimeProject.Domain.Commands.TimeSheets
{
    public class InsertTimeSheetCommand : TimeSheetCommand
    {
        public override bool IsValid()
        {
            return base.IsValid(new InsertTimeSheetValidation().Validate(this));
        }
    }
}
