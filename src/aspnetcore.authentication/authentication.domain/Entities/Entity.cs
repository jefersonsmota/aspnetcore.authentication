using System;

namespace authentication.domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public abstract bool IsValid();
    }
}
