export enum RequestType {
  Get = "GET",
  Post = "POST",
  Put = "PUT",
  Delete = "DELETE"
}

export const request = async (
  contractType: string,
  id?: any,
  method = "GET",
  data?: any
) => {
  const response = await fetch(`/api/${contractType}/${id && id}`, {
    method: method,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token")
    },
    body: data
  });
  if (!response.ok) {
    throw new Error(response.statusText);
  }
  return await response.json();
};
