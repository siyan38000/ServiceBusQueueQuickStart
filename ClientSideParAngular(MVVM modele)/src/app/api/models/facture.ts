import { Designation } from "./designation";

export class Facture {
	destinataire: string;
	adresseDestinataire: string;
	dateFacturation: Date;
	designation: Designation[];
}