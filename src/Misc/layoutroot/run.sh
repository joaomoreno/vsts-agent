user_id=`id -u`

if [ ! -f .agent ]; then
    echo "Must configure first. Run ./config.sh"
    exit 1
fi

./bin/Agent.Listener run $*
