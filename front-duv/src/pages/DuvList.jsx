import { useEffect, useState } from "react";
import axios from "axios";
import "./../App.css";
import { usePassageiroCrud } from "../hooks/usePassageiroCrud";

function DuvList() {
  const [duvs, setDuvs] = useState([]);
  const [selectedDuv, setSelectedDuv] = useState(null);

  useEffect(() => {
    axios
      .get("https://localhost:7204/api/Duv")
      .then((response) => {
        setDuvs(response.data);
        setSelectedDuv(response.data[0] || null);
      })
      .catch((error) => console.error("Erro ao buscar DUVs", error));
  }, []);

  const {
    modalAberto,
    passageiroEmEdicao,
    setPassageiroEmEdicao,
    abrirFormularioPassageiro,
    salvarPassageiro,
    removerPassageiro,
    setModalAberto
  } = usePassageiroCrud(selectedDuv, setSelectedDuv, setDuvs);

  return (
    <>
      <div className="sidebar">
        <h2>DUVs</h2>
        {duvs.length === 0 && <p>Nenhuma DUV encontrada.</p>}
        {duvs.map((duv) => (
          <div
            key={duv.id}
            className={`duv-item ${selectedDuv?.id === duv.id ? "selected" : ""}`}
            onClick={() => setSelectedDuv(duv)}
          >
            <strong>DUV: {duv.numero}</strong>
            <br />
            <span>
              Data da viagem: {new Date(duv.dataViagem).toLocaleDateString()}
            </span>
          </div>
        ))}
      </div>

      <div className="main">
        {selectedDuv ? (
          <>
            <h1>DUV: {selectedDuv.numero}</h1>
            <p>
              <strong>Data da viagem:</strong>{" "}
              {new Date(selectedDuv.dataViagem).toLocaleDateString()}
            </p>

            <img
              src={
                selectedDuv.navio?.imagemUrl ||
                "https://upload.wikimedia.org/wikipedia/commons/8/85/MSCVirtuosa.jpg"
              }
              alt="Navio"
              className="ship-img"
            />

            <p>
              <strong>Nome do Navio:</strong> {selectedDuv.navio?.nome}
            </p>
            <p>
              <strong>Bandeira:</strong> {selectedDuv.navio?.bandeira}
            </p>

            <h3>Passageiros</h3>
            <button className="botao botao-primario botao-adicionar" onClick={() => abrirFormularioPassageiro(null, 1)}>Adicionar Passageiro</button>
            <div className="pessoa-container">
              {selectedDuv.passageiros
                ?.filter((p) => !p.sid)
                .map((p) => (
                  <div key={p.id} className="pessoa-card">
                    <img src={p.fotoUrl} alt={p.nome} className="foto-pessoa" />
                    <span><strong>{p.nome}</strong></span>
                    <p>{p.nacionalidade}</p>
                    <button className="botao botao-secundario" onClick={() => abrirFormularioPassageiro(p)}>Editar</button>
                    <button className="botao botao-perigo" onClick={() => removerPassageiro(p.id)}>Remover</button>
                  </div>
                ))}
            </div>

            <div className="divisoria"></div>

            <h3>Tripulantes</h3>
            <button className="botao botao-primario botao-adicionar" onClick={() => abrirFormularioPassageiro(null, 2)}>Adicionar Tripulante</button>
            <div className="pessoa-container">
              {selectedDuv.passageiros
                ?.filter((p) => p.sid)
                .map((p) => (
                  <div key={p.id} className="pessoa-card">
                    <img src={p.fotoUrl} alt={p.nome} className="foto-pessoa" />
                    <span><strong>{p.nome}</strong></span>
                    <p>{p.nacionalidade}</p>
                    <p><em>SID:</em> {p.sid}</p>
                    <button className="botao botao-secundario" onClick={() => abrirFormularioPassageiro(p)}>Editar</button>
                    <button className="botao botao-perigo" onClick={() => removerPassageiro(p.id)}>Remover</button>
                  </div>
                ))}
            </div>
          </>
        ) : (
          <div className="placeholder-container">
            <img
              src="https://cdn-icons-png.flaticon.com/512/744/744922.png"
              alt="Ícone DUV"
              className="placeholder-img"
            />
            <h2>Bem-vindo ao Portal de DUVs</h2>
            <p>Selecione uma DUV na lista ao lado para visualizar os detalhes da viagem.</p>
          </div>
        )}
      </div>

      {modalAberto && (
        <div className="modal">
          <h3>{passageiroEmEdicao.id ? "Editar" : "Adicionar"} {passageiroEmEdicao.tipo === 2 ? "Tripulante" : "Passageiro"}</h3>

          <input
            placeholder="Nome"
            value={passageiroEmEdicao.nome}
            onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, nome: e.target.value })}
          />
          <input
            placeholder="Nacionalidade"
            value={passageiroEmEdicao.nacionalidade}
            onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, nacionalidade: e.target.value })}
          />
          <input
            placeholder="Foto URL"
            value={passageiroEmEdicao.fotoUrl}
            onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, fotoUrl: e.target.value })}
          />

          {passageiroEmEdicao.tipo === 2 && (
            <input
              placeholder="SID"
              value={passageiroEmEdicao.sid}
              onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, sid: e.target.value })}
            />
          )}

          <button className="botao botao-primario" onClick={salvarPassageiro}>Salvar</button>
          <button className="botao botao-perigo" onClick={() => setModalAberto(false)}>Cancelar</button>
        </div>
      )}
    </>
  );
}

export default DuvList;
