> **Note** This repository is developed for .netstandard2.0 with support .net5, net6, and .net7, and .net8.

| Name     | Details |
|----------|----------|
| DbObjectExecutor | [![NuGet Version](https://img.shields.io/nuget/v/DbObjectExecutor.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/DbObjectExecutor/) [![Nuget Downloads](https://img.shields.io/nuget/dt/DbObjectExecutor.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/DbObjectExecutor)|
| DbObjectExecutor.Attribute | [![NuGet Version](https://img.shields.io/nuget/v/DbObjectExecutor.Attribute.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/DbObjectExecutor.Attribute/) [![Nuget Downloads](https://img.shields.io/nuget/dt/DbObjectExecutor.Attribute.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/DbObjectExecutor.Attribute) |
| DbObjectExecutor.Imp.EntityFramework | [![NuGet Version](https://img.shields.io/nuget/v/DbObjectExecutor.Imp.EntityFramework.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/DbObjectExecutor.Imp.EntityFramework/) [![Nuget Downloads](https://img.shields.io/nuget/dt/DbObjectExecutor.Imp.EntityFramework.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/DbObjectExecutor.Imp.EntityFramework) |

The idea to implement the current repository started many years ago and is based on simple ideas and problems that must be solved.

The first and the base problem is in the stored procedure or function execution. So practically in every ORM you can find, the method of execution/implementation and execution of an object (mentioned above) is quite difficult and does not allow their execution in a fairly simple and adequate way.

Certainly, in case of the need to execute a stored procedure, the most frequent and perhaps the most convenient way was to switch/implement the execution to ADO.NET.

So, to clarify what this repository represents: it is like a wrapper and a group of settings created around ADO.NET to be able to execute a stored procedure, a function, or a simple database query more efficiently with the simple possibility of adding parameters (input/output or return).

Implementation of the current (logic) repository is separated into 3 components:
- `DbObjectExecutor` -> contains the basic/core logic of execution and necessary functionalities;
- `DbObjectExecutor.Attribute` -> contains the functionalities from `DbObjectExecutor` and additional permits decoration of a class and their properties with attributes that represent the name and settings of the future execution request;
- `DbObjectExecutor.Imp.EntityFramework` -> contains the functionalities from `DbObjectExecutor.Attribute` and extensions for `DbContext`(EntityFramework).


To understand more efficiently how you can use available functionalities please consult the [using documentation/file](docs/usage.md).

**In case you wish to use it in your project, u can install the package from <a href="https://www.nuget.org/packages/DbObjectExecutor" target="_blank">nuget.org</a>** or specify what version you want:

> `Install-Package DbObjectExecutor -Version x.x.x.x`<br/>
> `Install-Package DbObjectExecutor.* -Version x.x.x.x`

## Content
1. [USING](docs/usage.md)
1. [CHANGELOG](docs/CHANGELOG.md)
1. [BRANCH-GUIDE](docs/branch-guide.md)