# My Readme (Chris)

## Notes:

- I've left some comments in the code for the sake of this test. Usually my view on comments is that they should only be used to explain "why" something has been done in a certain way, and that my code should be readable enough that comments are not required to describe it further. If my code is not readable enough, I'll aim to fix that and make it more readable rather than add comments to it.

- I created a fake Currency DB in the same vein as the current Product one, in order to consider some scalability; we can add more currencies easily by updating the DB and adding a Constant to match.

- I have created a "ProductService" which kind of acts like a repository (which I guess is also a service anyway) - this uses both different DataAccess classes to achieve the response we required. I felt that the controller has no reason to know about 2 different data access layers, so I moved that logic into a service of its own and didn't call it a "Repository" because I'd personally call my "DataAccess" classes repositories and make those more broad, and then use Services as more of a "controller -> data access layer" translation thingy (which some might just call a Handler)! This is just preference and not really a "rule" so I figured I'd clear it up here.

- I replaced XUnit with NUnit just for some familiarity, for now - I can happily learn XUnit but it felt more efficient to do this for now.

- I have created unit tests with different names to prove both user stories but haven't done anything to explicitly link them. Ideally, I'd have integration tests for user stories and use gherkin or something similar for the syntax.

- I didn't add any logging, but to clarify I would log when we gracefully "fail" in any places so that stuff is easier to track later on. This also comes down to how in practice it's common to use tools that log most failures for you, so I'd only then log the things that are important and not likely to throw, or be part of, an exception.

- There is some issue with Swagger and it isn't displaying the docs properly. I'm unsure if this is intended but I assumed not and didn't invest time into fixing it. I just added comments regardless.

## With more time I:

- Would update the endpoint to response with an IActionResult and handle some other response codes such as 404, however in this case I've opted to either respond with all products or no products, and to use GBP is the provided currency is invalid.

- Would add proper logging around all of the different types of response etc. but I left this out in the interest of not going over the top with everything.

- Would add integration tests to test end-to-end including an in-memory database. Without an actual DB implementation this was unnecessary though.

---

# Original Readme:

# Greggs.Products
## Introduction
Hello and welcome to the Greggs Products repository, thanks for finding it!

## The Solution
So at the moment the api is currently returning a random selection from a fixed set of Greggs products directly 
from the controller itself. We currently have a data access class and it's interface but 
it's not plugged in (please ignore the class itself, we're pretending it hits a database),
we're also going to pretend that the data access functionality is fully tested so we don't need 
to worry about testing those lines of functionality.

We're mainly looking for the way you work, your code structure and how you would approach tackling the following 
scenarios.

## User Stories
Our product owners have asked us to implement the following stories, we'd like you to have 
a go at implementing them. You can use whatever patterns you're used to using or even better 
whatever patterns you would like to use to achieve the goal. Anyhow, back to the 
user stories:

### User Story 1
**As a** Greggs Fanatic<br/>
**I want to** be able to get the latest menu of products rather than the random static products it returns now<br/>
**So that** I get the most recently available products.

**Acceptance Criteria**<br/>
**Given** a previously implemented data access layer<br/>
**When** I hit a specified endpoint to get a list of products<br/>
**Then** a list or products is returned that uses the data access implementation rather than the static list it current utilises

### User Story 2
**As a** Greggs Entrepreneur<br/>
**I want to** get the price of the products returned to me in Euros<br/>
**So that** I can set up a shop in Europe as part of our expansion

**Acceptance Criteria**<br/>
**Given** an exchange rate of 1GBP to 1.11EUR<br/>
**When** I hit a specified endpoint to get a list of products<br/>
**Then** I will get the products and their price(s) returned
