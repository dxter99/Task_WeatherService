Case Study:

We would like you to develop a weather service that will retrieve daily information for a number of cities in the world. We expect you to share the code through a publicly hosted repository, i.e., GitHub, within 3 days of this email. Please read the sections below to gain a full understanding.

 

TASK DESCRIPTION

The weather service will receive a daily file that contains a list of cities. For each city in the file, we need to retrieve the weather information from the OpenWeather RESTful web service. Results will need to be stored in the output folder, so that each file only holds the information for each city for today’s date. We need to establish historic information, so file naming should cater for it.


SOLUTION:
Created web api which will take the csv file as input having city names based on which it will fetch the cityId from open weather site and pass it to the provided endpoint with the combination of cityId and api key it will fetch the data and store it output folder created in solution with proper naming convention for historic information. created unit test for same.
