import axios from "axios";

export const getClientes = async () => {
  try {
    const response = await axios.get("http://localhost:5002/api/clientes", {
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "http://localhost:5008/",
      },
    });
    console.log(response.data);
    return response.data;
  } catch (error: any) {
    throw new Error("Erro ao obter dados de Clientes: " + error.message);
  }
};
