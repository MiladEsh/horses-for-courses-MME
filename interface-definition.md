# Horses For Courses WebApi Interface Contract 

*See also:* [Examples](./interface-definition-examples.md)

## Coach

### 📄 POST /coaches
- *Request:*
    - JSON body: 
    ```json
    {
        "name": string,
        "email": string
    }
    ```
- *Response:* Coach.Id:`int`

### 📄 POST /coaches/{id}/skills
- *Request:*
    - Url parameter: `Id`:`int`
    - JSON body:
    ```json
    {
        // Volledige lijst van skills
        //      voor de relevante Coach
        "skills": [string] 
    }
    ```
- *Response:* Nothing

### 📄 Get /coaches
- *Request:* Nothing
- *Response:* 
    - JSON body: string 
    ```json
    {   [{ 
            "id": int, 
            "name": string, 
            "email": string,
            "numberOfCoursesAssignedTo": int
        }] 
    }
    ```
### 📄 Get /coaches/{id}
- *Request:*
    - Url parameter: `Id`:`int`
- *Response:* 
    - JSON body: string 
    ```json
    { "id": int
    , "name": string
    , "email": string
    , "skills": [string]
    // list of courses this coach is assigned to
    //  mapped to { (course)id, (course)name } object
    , "courses": [{"id":int, "name": string}] 
    }
    ```

## Course

### 📄 POST /courses
- *Request:*
    - JSON body: 
    ```json
    {
        "name": string,
        "startDate": string(#yyyy-mm-dd#), 
        "endDate": string(#yyyy-mm-dd#)
    }
    ```
- *Response:* `Course.Id`:`int`

### 📄 POST /courses/{id}/skills
- *Request:*
    - Url parameter: `Id`:`int`
    - JSON body:
    ```json
    {
        // Volledige lijst van skills
        //      voor de relevante Course
        "skills": [string] 
    }
    ```
- *Response:* Nothing

### 📄 POST /courses/{id}/timeslots
- *Request:*
    - Url parameter: `Id`:`int`
    - JSON body:
    ```json
    {
        // Volledige lijst van timeslots
        //      voor de relevante Course
        "timeslots": [
            { "day": string
            , "start": int // == hour
            , "end": int   // == hour
            }] 
    }
    ```
- *Response:* Nothing

### 📄 POST /courses/{id}/confirm
- *Request:*
    - Url parameter: `Id`:`int`
- *Response:* Nothing

### 📄 POST /courses/{id}/assign-coach
- *Request:*
    - Url parameter: `Id`:`int`
    - JSON body: 
    ```json
    {
        "coachId": int 
    }
    ```
- *Response:* Nothing

### 📄 Get /courses
- *Request:* Nothing
- *Response:* 
    - JSON body: string 
    ```json
    {   [{ 
            "id": int, 
            "name": string,
            "startDate": string(#yyyy-mm-dd#), 
            "endDate": string(#yyyy-mm-dd#),
            "hasSchedule": bool, // a.k.a, has timeslots/planning/...
            "hasCoach": bool
        }] 
    }
    ```
### 📄 Get /courses/{id}
- *Request:*
    - Url parameter: `Id`:`int`
- *Response:* 
    - JSON body: string 
    ```json
    { "id": int
    , "name": string
    , "startDate": string(#yyyy-mm-dd#)
    , "endDate": string(#yyyy-mm-dd#)
    , "skills": [string]
    , "timeslots": [
        { "day": string
        , "start": int // == hour
        , "end": int   // == hour
        }] 
    , "coach": {"id":int, "name": string}
    }
    ```