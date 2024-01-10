import axios from "axios";

const baseURL = 'https://localhost:7198/api';
export default axios.create(
    {
        baseURL: baseURL,
        headers:{
            "Content-Type":"application/json"
        }
    }
);
