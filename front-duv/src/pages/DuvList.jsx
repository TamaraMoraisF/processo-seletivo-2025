import { useEffect, useState } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

function DuvList() {
  const [duvs, setDuvs] = useState([]);

  useEffect(() => {
    axios
      .get("https://localhost:7204/api/Duv")
      .then(response => setDuvs(response.data))
      .catch(error => console.error("Erro ao buscar DUVs", error));
  }, []);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Lista de DUVs</h1>
      {duvs.length === 0 ? (
        <p>Nenhuma DUV encontrada.</p>
      ) : (
        <ul>
          {duvs.map(duv => (
            <li key={duv.id}>
              <Link to={`/duv/${duv.id}`}>
                <strong>DUV:</strong> {duv.numero} <br />
                <strong>Data:</strong>{" "}
                {new Date(duv.dataViagem).toLocaleDateString()}
              </Link>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default DuvList;
