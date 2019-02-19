import axios from "axios";

export async function getUsers() {
  const promise = axios.get("http://localhost:60356/api/account");
  const reponse = await promise;
  return reponse;
}
