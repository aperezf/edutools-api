using System;
using System.Collections.Generic;

namespace edutools_api.store.Edutools;

public partial class User
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }
}
