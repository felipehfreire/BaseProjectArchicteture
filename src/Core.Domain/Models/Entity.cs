using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Core.Domain.Models
{
    public abstract class Entity<Type> : AbstractValidator<Type> where Type : Entity<Type>
    {

        public int Id { get; protected set; }
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }

        
        public ValidationResult ValidationResult { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<Type>;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<Type> a, Entity<Type> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<Type> a, Entity<Type> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<int>.Default.GetHashCode(Id);
        }
        public override string ToString()
        {
            return GetType().Name + "[id= " + Id + "]";
        }

        public abstract bool IsValid();
        public abstract void Validate();
    }
}
