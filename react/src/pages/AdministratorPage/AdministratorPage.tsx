import { Table, TableHead, TableRow, TableCell, TableBody, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { GetAllUserInfo, UpdateUserRole } from '../../services/backend/UserController';
import { UserData, UserRole } from '../../types/Models/UserData';

const AdministratorPage = () => {
    const [users, setUsers] = useState<UserData[]>([]);
    const [update, setUpdateUsers] = useState<boolean>(false);
    useEffect(() => {
        GetAllUserInfo()
            .then((u) => {
                console.log(u);
                setUsers(u);
            })
            .finally(() => console.log('done'))
            .catch((e) => {
                toast.error('Error retrieving users');
                console.log(e);
            });
    }, [update]);

    const handleRoleChange = (user: UserData, event: SelectChangeEvent) => {
        const role = event.target.value as UserRole;
        console.log(user);
        UpdateUserRole({ ...user, role })
            .then(() => {
                toast.success('User role updated');
                setUpdateUsers((prev) => !prev);
            })
            .catch((e) => {
                console.log(e);
                toast.error('Error updating user role');
            })
            .finally(() => console.log('done'));
    };

    return (
        <div>
            <h2>Administrator section</h2>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Username</TableCell>
                        <TableCell>Email</TableCell>
                        <TableCell>FirstName</TableCell>
                        <TableCell>LastName</TableCell>
                        <TableCell>Role</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {users.map((user) => (
                        <TableRow key={user.username}>
                            <TableCell>{user.username}</TableCell>
                            <TableCell>{user.email}</TableCell>
                            <TableCell>{user.firstName}</TableCell>
                            <TableCell>{user.lastName}</TableCell>
                            <TableCell>
                                <Select onChange={(e) => handleRoleChange(user, e)} value={user.role}>
                                    <MenuItem value={UserRole.Administrator}>Admin</MenuItem>
                                    <MenuItem value={UserRole.Moderator}>Moderator</MenuItem>
                                    <MenuItem value={UserRole.Player}>User</MenuItem>
                                </Select>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </div>
    );
};
export default AdministratorPage;
