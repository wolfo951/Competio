import { Box, Collapse, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import { Fragment, useEffect, useState } from 'react';
import { GetAllTournaments } from '../../services/backend/Tournaments';
import { Tournament } from '../../types/Models/Tournament';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Link } from 'react-router-dom';
import CreateTournament from '../../Components/CreateTournament';

const TournamentsPage = () => {
    const [tournaments, setValues] = useState<Tournament[]>();

    const getAll = () => {
        GetAllTournaments().then((res) => setValues(res));
    };

    useEffect(() => {
        getAll();
    }, []);

    return (
        <div>
            <h2>Tournaments</h2>

            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>Tournament</TableCell>
                            <TableCell>Time of play</TableCell>
                            <TableCell>Player count</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {tournaments?.map((row) => (
                            <Row key={row.pkTournamentId} row={row} />
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <p />
        </div>
    );
};

function Row(props: { row: Tournament }) {
    const { row } = props;
    const [open, setOpen] = useState(false);

    return (
        <Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell>{row.title}</TableCell>
                <TableCell>{row.startsAt}</TableCell>
                <TableCell>{row.players.length}</TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h6" gutterBottom component="div">
                                Games
                            </Typography>
                            <Link to={`/tournament/${row.pkTournamentId}`}>Tournament details</Link>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </Fragment>
    );
}

export default TournamentsPage;
