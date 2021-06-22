import { Request, Response } from 'express';
import EmailService from "../services/EmailService";

const users = [
    { name: 'DiegoAAAAA', email: 'morkan@gmail.com' },
    { name: 'LucasAAAAAA', email: 'lucas@gmail.com' },
];

export default {
    async index(req: Request, res: Response) {
        return res.json(users);
    },

    async henrique(req: Request, res: Response) {
        return res.json("henrique boiolão");
    },

    async create(req: Request, res: Response) {
        const emailService = new EmailService();
        
        emailService.sendMail({
            to: { 
                name: 'Diego Andrade', 
                email: 'morkan@gmail.com' 
            }, 
            message: { 
                subject: 'Bem vindo ao sistema', 
                body: 'Seja bem vindo' 
            }
        });

        return res.send();
    }
};