using Entities.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string filedsString);
        ShapedEntity ShapeData(T entity, string fieldsString);
    }
}
