using System.Linq.Expressions;
using Cf.Dotnet.Architecture.Domain.SeedWork.Specification;

namespace Cf.Dotnet.Architecture.Domain.Specifications;

public class UnitsSpecification : Specification<int>
{
    private const int MinUnits = 0;
    private const int MaxUnits = 15;

    public override Expression<Func<int, bool>> ToExpression()
    {
        return units => units > MinUnits && units <= MaxUnits;
    }
}