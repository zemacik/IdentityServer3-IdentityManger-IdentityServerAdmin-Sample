# IdentityServer3 working sample

This is a working example of IdentityServer3, with Scopes and Clients management using IdentityServer3.Admin, and Identity store using IdentityManager on top of MembershipReboot


Main component versions used:

| Package                                     | Version        |
| ------------------------------------------- | -------------- |
| [IdentityServer3][1]                        | 2.4.0          |
| [IdentityServer3.Admin][2]                  | 1.0.0-beta4    |
| [IdentityServer3.Admin.EntityFramework][3]  | 1.0.0-beta3    |
| [IdentityManager][4]                        | 1.0.0-beta5-5  |
| [IdentityManager.MembershipReboot][5]       | 1.0.0-beta5-1  |


Identity manager middleware is accessible at:
> *http://localhost:44333/idm*

IdentityServer.Admin middleware is accessible at:
> *http://localhost:44333/adm*

###### Note:
After first run there is new default user account created:

| Type | Value |
| -------- | --------- |
| Username | **admin** |
| Password | **admin** |


###### Note 2:
If you will deploy this sollution you need to generate your own identity server certificate **idsrv3test.pfx** stored in *.\MyIdentityServer\ConfigIdentityServer\*


[1]: https://github.com/IdentityServer/IdentityServer3/tree/2.4.0 " "
[2]: https://github.com/IdentityServer/IdentityServer3.Admin " "
[3]: https://github.com/IdentityServer/IdentityServer3.Admin.EntityFramework " "
[4]: https://github.com/IdentityManager/IdentityManager/tree/v1-beta5-5 " "
[5]: https://github.com/IdentityManager/IdentityManager.MembershipReboot/tree/v1-beta5-1 " "

