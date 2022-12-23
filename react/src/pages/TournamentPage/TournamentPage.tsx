import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import PlayerList from '../../Components/PlayerList/PlayerList';
import { GetTournamentDetails } from '../../services/backend/Tournaments';
import { Tournament } from '../../types/Models/Tournament';

const TournamentPage = () => {
    let { id } = useParams();
    const navigate = useNavigate();
    const [tournament, setValue] = useState<Tournament>();
    const [update, setUpdate] = useState<boolean>(false);
    useEffect(() => {
        if (id === undefined) navigate('/not-found');
        else GetTournamentDetails(id).then((res) => setValue(res));
    }, [id, update]);

    return (
        <div>
            {tournament === undefined && <h1>Loading..</h1>}
            {tournament !== undefined && (
                <div>
                    <h1>{tournament.title}</h1>
                    <h2>This is {id} tournament</h2>
                    Players:
                    <PlayerList players={tournament?.players} tournament={tournament} update={setUpdate} />
                </div>
            )}
        </div>
    );
};

export default TournamentPage;
