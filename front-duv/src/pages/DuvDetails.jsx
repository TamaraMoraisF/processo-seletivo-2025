import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import axios from "axios";

function DuvDetails() {
  const { id } = useParams();
  const [duv, setDuv] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get(`https://localhost:7204/api/Duv/${id}`)
      .then(response => {
        setDuv(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error("Erro ao buscar DUV:", error);
        setLoading(false);
      });
  }, [id]);

  if (loading) return <p>Carregando...</p>;
  if (!duv) return <p>DUV não encontrada.</p>;

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Detalhes da DUV</h1>
      <p><strong>Número:</strong> {duv.numero}</p>
      <p><strong>Data da Viagem:</strong> {new Date(duv.dataViagem).toLocaleDateString()}</p>
      <br />
      <Link to="/">← Voltar para lista</Link>
    </div>
  );
}

export default DuvDetails;
