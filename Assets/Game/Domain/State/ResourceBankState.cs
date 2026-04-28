namespace Reacative.Domain.State
{
    public record ResourceBankState(
        double Energy,
        double Credits,
        double Certs
    )
    {
        public override string ToString()
        {
            return $"Energy: {Energy}\nCredits:{Credits}\nCerts:{Certs}";
        }
    }
}