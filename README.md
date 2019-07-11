# DockIT
An opensource Api Documentation collaboration tool

[![Build Status](https://dev.azure.com/jaytee116/DocIT/_apis/build/status/DocIT-ASP.NET%20Core-CI?branchName=master)](https://dev.azure.com/jaytee116/DocIT/_build/latest?definitionId=9&branchName=master)


Api documentation in 2019 is everywhere and no where, most devs would opt to use maybe postman collection for their documentation or genereate a swagger doc and serve it from their API or pay to upload it maybe swagger hub or apiary or some other services..


DocIT is targetting smaller teams who dont have the time to manually generate a postman collection, or who dont want to build a security around their own hosted swagger UI instance

DocIT basically builds collaboration around your swagger UI docs

we give you a docker image you can host internally...have your team mates sign up, create projects, invite other team mates to view api docs, you can also generate special links for public viewing

We also allow you to provide a link to your github project...so that the backend team can push changes to the swagger file and the frontend ghyz view the updates almost instantly 


The docker image can be deployed anywhere heroku, Now ZEIT so you dont have to pay crazily to host your swagger UI anymore, you get collaboration for free...your swagger is also updated on each push to your repo 🙂
