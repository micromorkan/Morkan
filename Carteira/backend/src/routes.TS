import { Router } from 'express';
import UserController from './controllers/UserController';

const routes = Router();

routes.get('/api/users', UserController.index);
routes.get('/api/henrique', UserController.henrique);
routes.post('/users/create', UserController.create);

export default routes; 