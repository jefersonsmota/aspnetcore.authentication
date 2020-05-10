using System;

namespace authentication.domain.Entities
{
    /// <summary>
    /// Telefones domínio.
    /// </summary>
    public class Phone
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int AreaCode { get; set; }
        public string CountryCode { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
