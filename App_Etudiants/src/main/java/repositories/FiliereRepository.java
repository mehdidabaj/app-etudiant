package repositories;

import entities.Filiere;

public class FiliereRepository extends Repository<Filiere, Integer> {
	public FiliereRepository() {
		super(Filiere.class);
	}
}