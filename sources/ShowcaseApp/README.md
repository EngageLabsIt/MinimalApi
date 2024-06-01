# Showcase application

This simple WebApi application shows how you can build your API modules (someone may use their old name `Controllers` 🫣) and play ith the configuration.

1. In the [Program.cs](Program.cs) you will find a call to `app.MapApi()`extension method to register all the modules. The same call configures a caching behavior to all the application endpoints
2. The [PostsApiModule.cs](Posts/PostsApiModule.cs) is a simple module with some endpoints to perform CRUD operations without any particular configuration besides some Open API informations like name and description
3. The [BlogsApiModule.cs](Blogs/BlogsApiModule.cs) is a simple module with some endpoints to perform CRUD operations with an additional custom policy applied to all the endpoints (the policy itself simply returns `true`, allowing the API call). In the same module, the `DELETE` operation is protected from another policy, that "overrides" the one specified in the module (tecnically, both policies are evaulated with an `AND` clause among them) blocking you with a `401`.
