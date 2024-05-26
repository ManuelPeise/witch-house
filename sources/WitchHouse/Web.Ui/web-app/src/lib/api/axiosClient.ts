import axios from 'axios';

export default axios.create({
  baseURL: process.env.REACT_APP_Api_Base,
  headers: { 'Content-Type': 'application/json' },
});
