import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { useEffect, useMemo, useState } from 'react';
import { GetAllPublicTournaments } from '../../services/backend/Tournaments';
import { Tournament } from '../../types/Models/Tournament';

const PublicTournamentsPage = () => {
    const [publicTournaments, setTournaments] = useState<Tournament[]>();

    useEffect(() => {
        GetAllPublicTournaments()
            .then((t) => {
                setTournaments(t);
            })
            .finally(() => console.log('done'));
    }, []);

    const table = useMemo(() => {
        if (publicTournaments === undefined || publicTournaments.length === 0) return <p>No public tournaments found</p>;
        return (
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Tournament name</TableCell>
                            <TableCell>Address</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {publicTournaments.map((tournament) => (
                            <TableRow key={tournament.title}>
                                <TableCell>{tournament.title}</TableCell>
                                <TableCell>{tournament.address}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        );
    }, [publicTournaments]);

    return (
        <>
            <h1>Public tournaments</h1>
            <p>This is where all the public tournaments are displayed and can be selected</p>

            {table}
        </>
    );
};
export default PublicTournamentsPage;
