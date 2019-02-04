# FindPairGame
Find Pair Game in C#

public function getContacts(IUser $user, $filter) {
		$allContacts = $this->contactsManager->search($filter ?: '', [
			'UID',
			'FN',
			'EMAIL',
		]);

		$sharedContacts = [];

        $conn = \OC::$server->getDatabaseConnection();
        $sharedWith = $conn->executeQuery('SELECT * FROM oc_share');

        while ($row = $sharedWith->fetch()) {
            for ($i = 0; $i < count($allContacts); $i++) {
                if ($row['share_with'] == $allContacts[$i]['UID']) {
                    $sharedContacts[] = $allContacts[$i];
                }
            }
        }

		$entries = array_map(function(array $contact) {
			return $this->contactArrayToEntry($contact);
		}, $sharedContacts);

		return $this->filterContacts(
			$user,
			$entries,
            $filter
		);
	}
