using System;

namespace Shop.Api.Data.Model
{
    public interface IAuditable
    {
        DateTime LastUpdated { get; set; }
        DateTime Created { get; set; }
    }
}